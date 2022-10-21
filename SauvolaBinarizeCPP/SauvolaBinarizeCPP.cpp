// SauvolaBinarizeCPP.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include <cmath>
#include <algorithm>
#include "SauvolaBinarizeCPP.h"

using namespace std;

SAUVOLABINARIZECPP_API int sauvolaBinarize(int height, int width, unsigned char* input, char* output, int whalf, double K)
{
    if (input == NULL)
        return -1; // error: no input
    if (output == NULL)
        return -2; // error: no output

    auto p = new int[height * width];
    auto sq = new long long[height * width];

    for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            p[i * width + j] = input[i * width + j];
            sq[i * width + j] = (long)input[i * width + j] * input[i * width + j];
            if (i > 0)
            {
                p[i * width + j] += p[(i - 1) * width + j];
                sq[i * width + j] += sq[(i - 1) * width + j];
            }
            if (j > 0)
            {
                p[i * width + j] += p[i * width + j - 1];
                sq[i * width + j] += sq[i * width + j - 1];
            }
            if (i > 0 && j > 0)
            {
                p[i * width + j] -= p[(i - 1) * width + j - 1];
                sq[i * width + j] -= sq[(i - 1) * width + j - 1];
            }
        }
    for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            auto xmin = max(0, i - whalf);
            auto ymin = max(0, j - whalf);
            auto xmax = min(height - 1, i + whalf);
            auto ymax = min(width - 1, j + whalf);

            auto area = (xmax - xmin + 1) * (ymax - ymin + 1);

            auto diff = p[width * xmax + ymax];
            auto sqdiff = sq[width * xmax + ymax];
            if (xmin > 0)
            {
                diff -= p[width * (xmin - 1) + ymax];
                sqdiff -= sq[width * (xmin - 1) + ymax];
            }
            if (ymin > 0)
            {
                diff -= p[width * xmax + ymin - 1];
                sqdiff -= sq[width * xmax + ymin - 1];
            }
            if (xmin > 0 && ymin > 0)
            {
                diff += p[width * (xmin - 1) + ymin - 1];
                sqdiff += sq[width * (xmin - 1) + ymin - 1];
            }
            auto mean = (double)diff / area;
            auto stdev = sqrt((sqdiff - diff * mean) / (area - 1));

            auto threshold = mean * (1 + K * ((stdev / 128) - 1));

            output[i * width + j] = input[i * width + j] > threshold ? 255 : 0;
        }
    delete[] p;
    delete[] sq;
    return 0;
}

SAUVOLABINARIZECPP_API int SaturatedMultiplyDouble(int h, int w, double* input, double* output)
{
    if (input == NULL)
        return -1; // error: no input
    if (output == NULL)
        return -2; // error: no output
    if (h < 1 || w < 1)
        return -3; // error: size is not enough
    //auto curr = 0;
    for (int i = 0; i < h; i++)
        for (int j = 0; j < w; j++)
            output[i * w + j] = min(input[i * w + j] * 1.5, 1.0);

    return 0;

}