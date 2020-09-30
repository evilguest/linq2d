#pragma once
// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the SAUVOLABINARIZECPP_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// SAUVOLABINARIZECPP_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef SAUVOLABINARIZECPP_EXPORTS
#define SAUVOLABINARIZECPP_API EXPORT
#else
#define SAUVOLABINARIZECPP_API IMPORT
#endif


extern "C" SAUVOLABINARIZECPP_API int sauvolaBinarize(int h, int w, char* input, char* output, int whalf, double K);
extern "C" SAUVOLABINARIZECPP_API int c4filter(int h, int w, char* input, int* output);
