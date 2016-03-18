namespace Test

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.HammerJS


[<JavaScript>]
module Client =    
   
    let Main =
        let cont =
            divAttr
                [
                    attr.style "background : silver; height : 300px; text-align: center; font: 30px/300px Helvetica, Arial, sans-serif;"
                ]
                []

        let hammer = Hammer.Manager(cont.Dom)

        let obj = 
            New 
                [
                    "direction" => Hammer.DIRECTION_ALL           
                ]
        
//        hammer.Get("pan").Set(obj)
        
        let a = Hammer.Swipe(obj)

        hammer.Add(a)

//        hammer.Get("swipe").Set(obj)

//        let pinch = Hammer.Pinch()
//        let rotate = Hammer.Rotate()
//
//        let a = [| pinch; rotate |] : Recognizer []
//
//        hammer.Add(a)

        hammer.On("swipeleft", fun ev ->
                DocExtensions.Clear(cont)
                let b = div [text ev.Type]
                Console.Log(ev.Detail)
                cont.Dom.AppendChild(b.Dom) |> ignore
            )

        cont |> Doc.RunById "main"