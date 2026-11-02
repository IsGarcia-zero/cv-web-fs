module CvWeb.Composition.Startup

open Microsoft.AspNetCore.Components.WebAssembly.Hosting

[<EntryPoint>]
let main args =
    let builder = WebAssemblyHostBuilder.CreateDefault args
    builder.RootComponents.Add<App.CvApp> "#main"
    builder.Build().RunAsync() |> ignore
    0
