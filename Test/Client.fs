// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace Test

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.HammerJS


[<JavaScript>]
module Client =    
   
    let Main =

        // Simple Hammer

        let d1 =
            Elt.div
                [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        let hammer1 = Hammer(d1.Dom)

        hammer1.On("panleft panright tap press", fun ev ->
                DocExtensions.Clear(d1)
                let b = Elt.div [] [text (ev.Type + "gesture detected")]
                d1.Dom.AppendChild(b.Dom) |> ignore
            )

        let d2 =
            Elt.div
                [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        // Swipe

        let hammer2 = Hammer.Manager(d2.Dom)


        let cfg = SwipeConf(Direction = Hammer.DIRECTION_ALL)
        
        hammer2.Add(Hammer.Swipe(cfg))

        hammer2.On("swipeleft swiperight swipeup swipedown panleft panright tap press", fun ev ->
                DocExtensions.Clear(d2)
                let b = Elt.div [] [text (ev.Type + "gesture detected")]
                d2.Dom.AppendChild(b.Dom) |> ignore
            )

        let d3 =
            Elt.div
                [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        // Single vs Double

        let hammer3 = Hammer.Manager(d3.Dom)

        hammer3.Add(Hammer.Tap(TapConf(Event = "doubletap", Taps = 2)))
        hammer3.Add(Hammer.Tap(TapConf(Event = "singletap")))
        hammer3.Get("doubletap").RecognizeWith("singletap")
        hammer3.Get("singletap").RequireFailure("doubletap")

        hammer3.Add(Hammer.Pan(PanConf(Direction=Hammer.DIRECTION_ALL)))

        hammer3.On("singletap doubletap panleft panright press", fun ev ->
                DocExtensions.Clear(d3)
                let b = Elt.div [] [text (ev.Type + "gesture detected")]
                d3.Dom.AppendChild(b.Dom) |> ignore
            )

        let cont =
            div
                []
                [
                    h2 [] [text "Simple hammer class"]
                    d1
                    h2 [] [text "This one only detects swipe"]
                    d2
                    h2 [] [text "Doubletap will not trigger singletap"] 
                    d3
                ]

        cont |> Doc.RunById "main"
