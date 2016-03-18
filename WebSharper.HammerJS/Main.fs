namespace WebSharper.HammerJS

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator
open WebSharper.JavaScript.Dom
open WebSharper.JQuery


module Definition =

    let mutable classList = [] : CodeModel.NamespaceEntity list

    let addToClassList c =
        classList <- upcast c :: classList

    let ( |>! ) x f =
        f x
        x

    let O = T<unit>
    let String = T<string>
    let Obj = T<obj>
    let Bool = T<bool>
    let Error = T<exn>
    let ( !| ) x = Type.ArrayOf x
    let Int = T<int>
    let Float = T<float>

    let Element = T<Element>
    let Event = T<Event>
    
    let Recognizer =
        Class "Recognizer"
            |+> Instance [
                "recognizeWith" => Obj ^-> O
                "dropRecognizeWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
        |>! addToClassList

    let PanConf =
        Pattern.Config "PanConf"
            {
                Required = []
                Optional = 
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                        "direction", Int
                    ]
            }
        |>! addToClassList

    let PinchConf =
        Pattern.Config "PinchConf"
            {
                Required = []
                Optional =
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                    ]
            }
        |>! addToClassList

    let PressConf =
        Pattern.Config "PressConf"
            {
                Required = []
                Optional =
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                        "time", Int
                    ]
            }
        |>! addToClassList

    let RotateConf =
        Pattern.Config "RotateConf"
            {
                Required = []
                Optional =
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                    ]
            }
        |>! addToClassList

    let SwipeConf =
        Pattern.Config "SwipeConf"
            {
                Required = []
                Optional =
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                        "direction", Int
                        "velocity", Float
                    ]
            }
        |>! addToClassList

    let TapConf =
        Pattern.Config "TapConf"
            {
                Required = []
                Optional =
                    [
                        "event", String
                        "pointers", Int
                        "treshold", Int
                        "taps", Int
                        "posTreshold", Int
                        "time", Int
                        "interval", Int
                    ]
            }
        |>! addToClassList
        

    let Pan = 
        Class "Hammer.Pan"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (O + PanConf)
            ]
            |+> Instance [
                "set" => PanConf ^-> O
            ]

    let Pinch =
        Class "Hammer.Pinch"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (O + PinchConf)
            ]
            |+> Instance [
                "set" => PinchConf ^-> O
            ]

    let Press =
        Class "Hammer.Press"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (O + PressConf)
            ]
            |+> Instance [
                "set" => PressConf ^-> O
            ]

    let Rotate =
        Class "Hammer.Rotate"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (RotateConf + O)
            ]
            |+> Instance [
                "set" => RotateConf ^-> O
            ]

    let Swipe =
        Class "Hammer.Swipe"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (O + SwipeConf)
            ]
            |+> Instance [
                "set" => SwipeConf ^-> O
            ]
            
    let Tap =
        Class "Hammer.Tap"
            |=> Inherits Recognizer
            |+> Static [
                Constructor (O + TapConf)
            ]
            |+> Instance [
                "set" => TapConf ^-> O
            ]

    let Hammer = 
        Class "Hammer"
        |+> Static [
            Constructor Element
            // The Constants
            "INPUT_END" =? Int
            "INPUT_MOVE" =? Int
            "INPUT_START" =? Int
            "STATE_ENDED" =? Int
            "STATE_BEGAN" =? Int
            "STATE_FAILED" =? Int
            "INPUT_CANCEL" =? Int
            "DIRECTION_UP" =? Int
            "DIRECTION_ALL" =? Int
            "STATE_CHANGED" =? Int
            "STATE_POSSIBLE" =? Int
            "DIRECTION_NONE" =? Int
            "DIRECTION_LEFT" =? Int
            "DIRECTION_DOWN" =? Int
            "STATE_CANCELLED" =? Int
            "DIRECTION_RIGHT" =? Int
            "STATE_RECOGNIZED" =? Int
            "DIRECTION_VERTICAL" =? Int
            "DIRECTION_HORIZONTAL" =? Int
        ]
        |+> Instance [
            "get" => String ^-> Recognizer
            "remove" => ( Obj + String + !|(Obj + String)) ^-> O
            "add" => (Recognizer + String + !|Recognizer + !|String) ^-> O
            "each" => Obj * (Obj * Obj * Obj ^-> O) ^-> O
            "on" => !?Obj * String * (Event ^-> O) ^-> O
            "off" => !?Obj * String * (Event ^-> O) ^-> O
            "merge" => Obj * Obj ^-> O
            "extend" => Obj * Obj ^-> O
            "inherit" => Obj * Obj * Obj ^-> O
            "prefixed" => Obj * String ^-> O
            "bindFn" => (Obj ^-> O) * Obj ^-> O
            "destroy" => O ^-> O
        ]
        |=> Nested [
            Pan
            Pinch
            Press
            Rotate
            Swipe
            Tap
            Class "Hammer.Manager"
            |+> Static [
                Constructor Element
            ]
            |+> Instance [
                "get" => String ^-> Recognizer
                "add" => (Recognizer + String + !|(Recognizer + String)) ^-> O
                "remove" => ( Obj + String + !|(Obj + String)) ^-> O
                "on" => !?Obj * String * (Event ^-> O) ^-> O
                "off" => !?Obj * String * !|(Event ^-> O) ^-> O
                "stop" => Bool ^-> O
                "destroy" => O ^-> O
                "emit" => String * Obj ^-> O
                "recognise" => Obj ^-> O
            ]
            Class "Hammer.deafults"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "touchAction" =? String
                "domEvents" =? Bool
                "enable" =? Bool
                "preset" =? !|Obj
            ]
            |=> Nested [
                Class "Hammer.cssProps"
                |+> Instance [
                    "contentZooming" =? String
                    "tapHighlightColor" =? String
                    "touchCallout" =? String
                    "touchSelect" =? String
                    "userDrag" =? String
                    "userSelect" =? String
                ]
            ]
        ]
        |>! addToClassList

    let HammerAssembly =
        Assembly [
            Namespace "WebSharper.HammerJS" classList
            Namespace "WebSharper.HammerJS.Resources" [
                (Resource "Hammer" "https://hammerjs.github.io/dist/hammer.js").AssemblyWide()
            ]
        ]

[<Sealed>]
type HammerExtension() =
    interface IExtension with
        member ext.Assembly = Definition.HammerAssembly

[<assembly: Extension(typeof<HammerExtension>)>]
do ()