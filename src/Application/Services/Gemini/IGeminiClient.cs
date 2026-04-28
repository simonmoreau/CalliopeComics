using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gemini
{
    public interface IGeminiClient
    {
        Task<string> AnalyseImageAsync(string imageFilePath);
    }
}
