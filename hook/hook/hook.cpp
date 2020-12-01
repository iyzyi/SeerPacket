#include "stdafx.h"
#include "hook.h"
#include <windows.h>
#include <stdio.h>
#include <CONIO.H>
#include <Winsock2.h>

//定义如下结构，保存一次InlineHook所需要的信息
typedef struct _HOOK_DATA {
	char szApiName[128];		//待Hook的API名字
	char szModuleName[64];		//待Hook的API所属模块的名字
	int  HookCodeLen;			//Hook长度
	BYTE oldEntry[16];			//保存Hook位置的原始指令
	BYTE newEntry[16];			//保存要写入Hook位置的新指令
	ULONG_PTR HookPoint;		//待HOOK的位置
	ULONG_PTR JmpBackAddr;		//回跳到原函数中的位置
	ULONG_PTR pfnTrampolineFun;	//调用原始函数的通道
	ULONG_PTR pfnDetourFun;		//HOOK过滤函数
}HOOK_DATA, *PHOOK_DATA;
HOOK_DATA RecvHookData, SendHookData;

//回调函数的指针（该函数位于c#中）
CallBackFun1 RecvCallBack = NULL;
CallBackFun2 SendCallBack = NULL;

//等效于HOOK前的recv和send的函数的指针
PFN_Recv OriginalRecv = NULL;
PFN_Send OriginalSend = NULL;

//声明
int WINAPI My_Recv(SOCKET s, char *buf, int len, int flags);
int WINAPI My_Send(SOCKET s, const char *buf, int len, int flags);
BOOL Inline_InstallHook_Recv();
BOOL Inline_InstallHook_Send();
LPVOID GetAddress(char *, char *);
void InitHookEntry(PHOOK_DATA pHookData);
VOID InitTrampoline(PHOOK_DATA pHookData);
BOOL InstallCodeHook(PHOOK_DATA pHookData);
void SetRecvCallBack(CallBackFun1 pFun);
void SetSendCallBack(CallBackFun2 pFun);


int WINAPI My_Recv(SOCKET s, char *buf, int len, int flags)
{
	int ret = OriginalRecv(s, buf, len, flags);
	if (ret > 0) {
		if (RecvCallBack) {
			RecvCallBack(s, buf, ret);
		}
	}
	return ret;
}

int WINAPI My_Send(SOCKET s, const char *buf, int len, int flags)
{
	/*int ret = OriginalSend(s, buf, len, flags);
	if (ret > 0) {
		if (SendCallBack) {
			SendCallBack(s, buf, ret);
		}
	}
	return ret;*/

	return SendCallBack(s, buf, len);
}

BOOL Inline_InstallHook_Recv()
{
	ZeroMemory(&RecvHookData, sizeof(HOOK_DATA));
	strcpy_s(RecvHookData.szApiName, "recv");
	strcpy_s(RecvHookData.szModuleName, "ws2_32.dll");
	RecvHookData.HookCodeLen = 15;
	RecvHookData.HookPoint = (ULONG_PTR)GetAddress(RecvHookData.szModuleName, RecvHookData.szApiName);//HOOK的地址
																									  //MsgBoxHookData.pfnOriginalFun = (PVOID)OriginalMessageBox;//调用原始函数的通道
																									  //x64下不能内联汇编了，所以申请一块内存用做TrampolineFun的shellcode
	RecvHookData.pfnTrampolineFun = (ULONG_PTR)VirtualAlloc(NULL, 128, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	RecvHookData.pfnDetourFun = (ULONG_PTR)My_Recv;//自定义hook函数
	BOOL result = InstallCodeHook(&RecvHookData);
	OriginalRecv = (PFN_Recv)RecvHookData.pfnTrampolineFun;			//相当于HOOK前的recv函数
	return result;
}

BOOL Inline_InstallHook_Send()
{
	ZeroMemory(&SendHookData, sizeof(HOOK_DATA));
	strcpy_s(SendHookData.szApiName, "send");
	strcpy_s(SendHookData.szModuleName, "ws2_32.dll");
	SendHookData.HookCodeLen = 15;
	SendHookData.HookPoint = (ULONG_PTR)GetAddress(SendHookData.szModuleName, SendHookData.szApiName);//HOOK的地址
	SendHookData.pfnTrampolineFun = (ULONG_PTR)VirtualAlloc(NULL, 128, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	SendHookData.pfnDetourFun = (ULONG_PTR)My_Send;//自定义hook函数
	BOOL result = InstallCodeHook(&SendHookData);
	OriginalSend = (PFN_Send)SendHookData.pfnTrampolineFun;			//相当于HOOK前的send函数
	return result;
}

//获取指定模块中指定API的地址
LPVOID GetAddress(char *dllname, char *funname)
{
	HMODULE hMod = 0;
	if (hMod = GetModuleHandle(dllname))
	{
		printf("%s早已加载\n", dllname);
		return GetProcAddress(hMod, funname);
	}
	else
	{
		printf("成功加载%s\n", dllname);
		hMod = LoadLibrary(dllname);
		return GetProcAddress(hMod, funname);
	}
}

/*
填充入口点指令
使用的是mov rax xxxxx; jmp rax，长度12
为了清除指令碎屑，后面要跟着3个nop
*/
void InitHookEntry(PHOOK_DATA pHookData)
{
	pHookData->newEntry[0] = 0x48;
	pHookData->newEntry[1] = 0xb8;
	*(ULONG_PTR*)(pHookData->newEntry + 2) = (ULONG_PTR)pHookData->pfnDetourFun;
	pHookData->newEntry[10] = 0xff;
	pHookData->newEntry[11] = 0xe0;
	pHookData->newEntry[12] = 0x90;
	pHookData->newEntry[13] = 0x90;
	pHookData->newEntry[14] = 0x90;
}


/*
构造从hook后的函数中回到原有函数的指令
由原来函数的入口点指令加上一个jmp构成
原来的入口点指令：
48 89 5C 24 08       mov     [rsp+arg_0], rbx
48 89 6C 24 10       mov     [rsp+arg_8], rbp
44 89 4C 24 20       mov     [rsp+arg_18], r9d
*/
VOID InitTrampoline(PHOOK_DATA pHookData)
{
	//保存前15字节
	PBYTE pFun = (PBYTE)pHookData->pfnTrampolineFun;
	memcpy(pFun, (PVOID)pHookData->HookPoint, 15);

	//在后面添加一个跳转指令
	pFun += 15; //跳过前三行指令
	pFun[0] = 0xFF;
	pFun[1] = 0x25;
	*(ULONG_PTR*)(pFun + 6) = pHookData->JmpBackAddr;
}


BOOL InstallCodeHook(PHOOK_DATA pHookData)
{
	SIZE_T dwBytesReturned = 0;
	HANDLE hProcess = GetCurrentProcess();
	BOOL bResult = FALSE;
	if (pHookData == NULL
		|| pHookData->HookPoint == 0
		|| pHookData->pfnDetourFun == NULL
		|| pHookData->pfnTrampolineFun == NULL)
	{
		return FALSE;
	}
	pHookData->JmpBackAddr = pHookData->HookPoint + pHookData->HookCodeLen;
	LPVOID OriginalAddr = (LPVOID)pHookData->HookPoint;
	printf("Address To HOOK=0x%p\n", OriginalAddr);
	InitHookEntry(pHookData);//填充Inline Hook代码
	InitTrampoline(pHookData);//构造Trampoline
	if (ReadProcessMemory(hProcess, OriginalAddr, pHookData->oldEntry, pHookData->HookCodeLen, &dwBytesReturned))	//读取并保存原有入口点的几条指令
	{
		if (WriteProcessMemory(hProcess, OriginalAddr, pHookData->newEntry, pHookData->HookCodeLen, &dwBytesReturned))
		{
			printf("Install Hook write OK! WrittenCnt=%lld\n", dwBytesReturned);
			bResult = TRUE;
		}
	}
	return bResult;
}

void SetRecvCallBack(CallBackFun1 pFun) {
	RecvCallBack = pFun;
}

void SetSendCallBack(CallBackFun2 pFun) {
	SendCallBack = pFun;
}

int WINAPI RealSend(SOCKET s, const char *buf, int len) {
	return OriginalSend(s, buf, len, 0);		//flags懒得传入c#中了，直接默认0得了
}