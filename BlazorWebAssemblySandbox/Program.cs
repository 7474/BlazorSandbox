using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblySandbox
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Document: https://docs.microsoft.com/ja-jp/aspnet/core/blazor/fundamentals/logging?view=aspnetcore-5.0&pivots=webassembly
            // �h�L�������g���炾�ƕ�����Â炢���A
            // ����� WebAssemblyConsoleLoggerProvider ���\������Ă��� ILogger �𒍓����邾���ŃR���\�[���Ƀ��O�o�͂��s����悤�ɂȂ��Ă���B
            // https://github.com/dotnet/aspnetcore/blob/d827c653b787c07de908240b7746ce34d3e6271e/src/Components/WebAssembly/WebAssembly/src/Hosting/WebAssemblyHostBuilder.cs#L227-L230
            builder.Logging
                // Chrome�̊J���҃c�[���̊���ł� Info ���x���ȏ��\������悤�ɂȂ��Ă���B
                // Debug �ȉ��̃��x����\������ɂ̓u���E�U���̐ݒ���ύX���K�v�B
                .SetMinimumLevel(LogLevel.Trace)
                // �����㑼�̃v���o�C�_�[�͐ݒ肵�Ă������Ȃ�������A�ݒ肵���u�ԂɁi�����Ɓj���Ή��ł���|�̗�O����������B
                // �Ⴆ�΃��[�J���X�g���[�W�Ƀ��O���o�͂��� CustomLoggingProvider ����������A�̂悤�Ȃ��Ƃ͂ł��Ȃ����Ȃ��悤���B
                .AddDebug()
                //.AddConsole()
                //.AddSimpleConsole()
                ;

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
