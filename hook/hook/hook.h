#ifndef _C_FUNCTION_H_
#define _C_FUNCTION_H_
#include "define.h"
#include <Winsock2.h>

//回调函数的指针
typedef void(*CallBackFun1)(SOCKET s, char* buf, int len);
typedef int (*CallBackFun2)(SOCKET s, const char *buf, int len);
//等效于HOOK前的recv和send的函数的指针
typedef int (WINAPI *PFN_Recv)(SOCKET s, char *buf, int len, int flags);
typedef int (WINAPI *PFN_Send)(SOCKET s, const char *buf, int len, int flags);

_EXTERN_C_ void hello();
_EXTERN_C_ BOOL Inline_InstallHook_Recv();
_EXTERN_C_ BOOL Inline_InstallHook_Send();
_EXTERN_C_ void SetRecvCallBack(CallBackFun1 pFun);
_EXTERN_C_ void SetSendCallBack(CallBackFun2 pFun);
_EXTERN_C_ int WINAPI RealSend(SOCKET s, const char *buf, int len);
#endif //_C_FUNCTION_H_