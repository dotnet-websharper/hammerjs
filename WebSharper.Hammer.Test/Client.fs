namespace WebSharper.Hammer.Test

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Hammer

[<JavaScript>]
module Client =    
    

    let Main =
        
        let container =
            divAttr
                [
                    attr.background "silver"
                    attr.height "300px"
                    attr.text "center"
                ]
                []

        let mc = WebSharper.Hammer.

        container |> Doc.RunById "main"