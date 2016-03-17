#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Hammer")
        .VersionFrom("WebSharper")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun f -> f.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.Hammer")
        .SourcesFromProject()
        .Embed([])
        .References(fun r -> [])

let tests =
    bt.WebSharper.SiteletWebsite("WebSharper.Hammer.Test")
        .SourcesFromProject()
        .Embed([])
        .References(fun r ->
            [
                r.Project(main)
                r.NuGet("WebSharper.UI.Next").Reference()
            ])

bt.Solution [
    main
    tests

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Hammer"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.com/intellifactory/hammerjs"
                Description = "WebSharper Extensions for Hammer.js"
                RequiresLicenseAcceptance = true })
        .Add(main)
]
|> bt.Dispatch
