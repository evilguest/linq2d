#include "pch.h"
#include <cmath>
#include <algorithm>
#include "SauvolaBinarizeCPP.h"


extern "C" void c4_asm1(unsigned char* input, int width, int* output, int height);

//input aligned at least to 8, output to 16
//unsigned char * input = (unsigned char*)_aligned_malloc (size * size ,8);
//int * output = (int*)_aligned_malloc (size * size * sizeof(int),16);

SAUVOLABINARIZECPP_API int c4filterAsm(int h, int w, unsigned char* input, int* output)
{
    if (input == NULL)
        return -1; // error: no input
    if (output == NULL)
        return -2; // error: no output
    if (h < 2 || w < 2)
        return -3; // error: size is not enough
    auto curr = 0;
    output[curr] = (2 * (int)input[curr] + (int)input[curr + 1] + (int)input[curr + w]) / 4;
    for (int j = 1; j < w - 1;j++)
        output[j] = ((int)input[j - 1] + (int)input[j] + (int)input[j + 1] + (int)input[j + w]) / 4;
    output[w - 1] = (2 * (int)input[w - 1] + (int)input[w - 2] + (int)input[w + w - 1]) / 4;
    c4_asm1(&input[w], w, &output[w], h - 2);
    curr = (h - 1) * w;
    output[curr] = (2 * (int)input[curr] + (int)input[curr + 1] + (int)input[curr - w]) / 4;
    for (int j = 1; j < w - 1;j++)
        output[curr + j] = ((int)input[curr + j - 1] + (int)input[curr + j] + (int)input[curr + j + 1] + (int)input[curr - w + j]) / 4;
    output[curr + w - 1] = (2 * (int)input[curr + w - 1] + (int)input[curr + w - 2] + (int)input[curr - 1]) / 4;

    return 0;
}