namespace WebSharper.Hammer

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
    let Int = T<int>
    let Float = T<float>
    let Obj = T<obj>
    let Bool = T<bool>
    let Error = T<exn>
    let ( !| ) x = Type.ArrayOf x

    let Int2T = Type.Tuple [Int; Int]
    let Int2x2T = Type.Tuple [Int2T; Int2T]
    let Float2T = Type.Tuple [Float; Float]
    let Float3T = Type.Tuple [Float; Float; Float]
    let Float2x2T = Type.Tuple [Float2T; Float2T]
    let Comparator = Obj * Obj ^-> Int

    let Date = T<JavaScript.Date>
    let DateTime = T<System.DateTime>

    let Element = T<Element>
    let NodeList = T<NodeList>
    let Event = T<Event>

    let HammerCons =
        Pattern.Config "Hammer"
            {
                Required = 
                    [
                        "element", Element
                    ]
                Optional = 
                    [
                        "options", !|Obj
                    ]
            }
        |>! addToClassList

    let Options =
        Pattern.Config "Options"
            {
                Required = []
                Optional = 
                    [
                        "touchAction", String
                        "domEvents", Bool
                        "enable", Bool
                        "cssProps", Obj
                        "preset", !|Obj
                    ]
            }
        |>! addToClassList

    let Hammer = 
        Class "Hammer"
        |+> Static [
            Constructor HammerCons
        ]
        |+> Instance [
            "on" => Obj * String * (Obj ^-> O) ^-> O
            "off" => Obj * String * (Obj ^-> O) ^-> O
            "each" => Obj * (Obj * Obj * Obj ^-> O) ^-> O
            "merge" => Obj * Obj ^-> O
            "extend" => Obj * Obj ^-> O
            "inherit" => Obj * Obj * Obj ^-> O
            "prefixed" => Obj * String ^-> O
            "bindFn" => (Obj ^-> O) * Obj ^-> O
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
        |=> Nested [
            Class "Pan"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Pinch"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Press"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Rotate"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Swap"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Tap"
            |+> Static [
                Constructor O
            ]
            |+> Instance [
                "set" => Options ^-> O
                "recogniseWith" => Obj ^-> O
                "dropRecogniseWith" => Obj ^-> O
                "requireFailure" => Obj ^-> O
                "dropRequireFailure" => Obj ^-> O
            ]
            Class "Manager"
            |+> Static [
                Constructor HammerCons
            ]
            |+> Instance [
                "set" => Options ^-> O
                "get" => String ^-> O
                "add" => Obj ^-> O
                "remove" => ( Obj + String ) ^-> O
                "on" => String * (Obj ^-> O) ^-> O
                "off" => String * !|(Obj ^-> O) ^-> O
                "stop" => Bool ^-> O
                "destroy" => O ^-> O
                "emit" => String * Obj ^-> O
                "recognise" => Obj ^-> O
            ]
            Class "deafults"
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
                Class "cssProps"
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
            Namespace "WebSharper.Hammer" classList
            Namespace "WebSharper.Hammer.Resources" [
                (Resource "Hammer" "https://cdnjs.cloudflare.com/ajax/libs/hammer.js/2.0.6/hammer.min.js").AssemblyWide()
            ]
        ]

[<Sealed>]
type HammerExtension() =
    interface IExtension with
        member ext.Assembly = Definition.HammerAssembly

[<assembly: Extension(typeof<HammerExtension>)>]
do ()
