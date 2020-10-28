// SauvolaBinarizeCPP.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include <cmath>
#include <algorithm>
#include "SauvolaBinarizeCPP.h"

using namespace std;
// This is an example of an exported function.
SAUVOLABINARIZECPP_API int sauvolaBinarize(int height, int width, char* input, char* output, int whalf, double K)
{
    if (input == NULL)
        return -1; // error: no input
    if (output == NULL)
        return -2; // error: no output

    auto p = new int[height * width];
    auto sq = new long[height * width];

    for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            p[i * width + j] = input[i * width + j];
            sq[i * width + j] = input[i * width + j] * input[i * width + j];
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

SAUVOLABINARIZECPP_API int c4filter(int h, int w, char* input, int* output)
{
    if (input == NULL)
        return -1; // error: no input
    if (output == NULL)
        return -2; // error: no output
    output[0] = (2 * (int)input[0] + (int)input[1] + (int)input[w]) / 4;
    for (int j = 1; j < w - 1;j++)
        output[j] = ((int)input[j - 1] + (int)input[j] + (int)input[j + 1] + (int)input[j + w]) / 4;
    output[w - 1] = (2 * (int)input[w - 1] + (int)input[w - 2] + (int)input[w + w - 1]) / 4;
    for (int i = 1; i < h - 1; i++)
    {
        output[i * w] = ((int)input[i * w] + (int)input[(i - 1) * w] + (int)input[i * w + 1] + (int)input[(i + 1) * w]) / 4;
        for (int j = 1; j < w - 1; j++)
            output[i * w + j] = ((int)input[(i - 1) * w + j] + (int)input[(i + 1) * w + j] + (int)input[i * w + (j - 1)] + (int)input[i * w + (j + 1)]) / 4;
        output[i*w + w - 1] = (2 * (int)input[i * w + w - 1] + (int)input[i * w + w - 2] + (int)input[(i + 2)*w - 1]) / 4;
    }
    output[(h - 1) * w] = (2 * (int)input[(h - 1) * w] + (int)input[(h - 1) * w + 1] + (int)input[h * w]) / 4;
    for (int j = 1; j < w - 1;j++)
        output[(h - 1) * w + j] = ((int)input[(h - 1) * w + j - 1] + (int)input[(h - 1) * w + j] + (int)input[(h - 1) * w + j + 1] + (int)input[(h-2) * w + j]) / 4;
    output[h * w - 1] = (2 * (int)input[h * w - 1] + (int)input[h * w - 2] + (int)input[h * w - w - 1]) / 4;

    return 0;
}
