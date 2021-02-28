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
            // ドキュメントからだと分かりづらいが、
            // 既定で WebAssemblyConsoleLoggerProvider が構成されており ILogger を注入するだけでコンソールにログ出力を行えるようになっている。
            // https://github.com/dotnet/aspnetcore/blob/d827c653b787c07de908240b7746ce34d3e6271e/src/Components/WebAssembly/WebAssembly/src/Hosting/WebAssemblyHostBuilder.cs#L227-L230
            builder.Logging
                // Chromeの開発者ツールの既定では Info レベル以上を表示するようになっている。
                // Debug 以下のレベルを表示するにはブラウザ側の設定も変更が必要。
                .SetMinimumLevel(LogLevel.Trace)
                // 事実上他のプロバイダーは設定しても動かなかったり、設定した瞬間に（ちゃんと）未対応である旨の例外が発生する。
                // 例えばローカルストレージにログを出力する CustomLoggingProvider を実装する、のようなことはできなくもないようだ。
                .AddDebug()
                //.AddConsole()
                //.AddSimpleConsole()
                ;

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
