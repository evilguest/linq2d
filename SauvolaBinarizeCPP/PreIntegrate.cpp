#include "pch.h"
#include <cmath>
#include <algorithm>
#include "SauvolaBinarizeCPP.h"

using namespace std;

SAUVOLABINARIZECPP_API int preIntegrate(int height, int width, unsigned char* input, int* linear, long long* square)
{
    if (input == NULL)
        return -1; // error: no input
    if (linear == NULL || square == NULL)
        return -2; // error: no output

    for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            auto curr = i * width + j;
            linear[curr] = input[curr];
            square[curr] = (long long)input[curr] * input[curr];
            if (i > 0)
            {
                linear[curr] += linear[curr - width];
                square[curr] += square[curr - width];
            }
            if (j > 0)
            {
                linear[curr] += linear[curr - 1];
                square[curr] += square[curr - 1];
            }
            if (i > 0 && j > 0)
            {
                linear[curr] -= linear[curr - width - 1];
                square[curr] -= square[curr - width - 1];
            }
        }
    return 0;
}
