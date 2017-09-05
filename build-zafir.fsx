#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.HammerJS")
        .VersionFrom("WebSharper")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun f -> f.Net40)

let main =
    bt.WebSharper4.Extension("WebSharper.HammerJS")
        .SourcesFromProject()

let tests =
    bt.WebSharper4.SiteletWebsite("Test")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).ForceFoundVersion().Reference()
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
