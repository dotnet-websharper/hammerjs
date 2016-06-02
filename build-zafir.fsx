#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("Zafir.HammerJS")
        .VersionFrom("Zafir")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun f -> f.Net40)

let main =
    bt.Zafir.Extension("WebSharper.HammerJS")
        .SourcesFromProject()

let tests =
    bt.Zafir.SiteletWebsite("Test")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).ForceFoundVersion().Reference()
                r.Project(main)
            ])

bt.Solution [
    main
    tests

    bt.NuGet.CreatePackage()
        .Add(main)
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Hammer"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.com/intellifactory/hammerjs"
                Description = "WebSharper Extensions for Hammer.js"
                RequiresLicenseAcceptance = true })
]
|> bt.Dispatch
