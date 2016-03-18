# WebSharper.HammerJS

## Introduction

(The following is the introduction from [Hammer.js][hammerjs] with samples translated to F#, and some comments on typing.)

Hammer is a open-source library that can recognize gestures made by touch, mouse and pointerEvents. It doesn't have any dependencies, which makes it easier to use. You can check with [this][browsersupp] link the support of events in today's big browsers  

## Creating a Hammer object

To do so, just pass a Dom element to the `Hammer` like this:

```
let cont = divAttr [] []
let hammer = Hammer(cont.dom)
```

Now this element will be able to sense the new event types, and will have the default presets, which is defined in the `Hammer.defaults`.

To modify the existing options, just use the `Get` and the `Set` method on your `Hammer` object.

```
// Get the pan element, and set it's direction to all
let obj = New [ "direction" => Hammer.DIRECTION_ALL ]
hammer.Get("pan").Set(obj)
```

To add or remove event recognizers from your object, just use the `Add` and `Remove` methods. You can pass a string or a Recognizer type (see below) or an array of these one to the `Add` or `Remove` method.

```
let pinch = Hammer.Pinch()
let rotate = Hammer.Rotate()
let array = [| pinch; rotate |] : Recognizer []
hammer.Add(arr)
```

To set a handler for an event (or even multiple one), just use the `On` method (to remove the `Off` ) on your `Hammer` object.

```
hammer.On("panleft panright", fun (ev) ->
	Console.Log(ev.Type)
)
```

## Recognizers

The In-built recognizers:

* [Pan][link1]
* [Pinch][link2]
* [Press][link3]
* [Rotate][link4]
* [Swipe][link5]
* [Tap][link6]

You can build new events based on these, like:

```
let obj = 
	New [ 
		"event" => "quadrupletap"
		"taps" => 4
	]
let quadrupletap = Hammer.Tap(obj)
```

To change the behaviour of the event, use the `Set` method. You have the option, to choose for every event, which one should be failed to trigger the event, and which one required to be happen before the event triggers. To do so, just use the `RequireFailure` and `RequireWith` methods. To drop this dependencies use the `dropRequireFailure` and `dropRequireWith`.

```
``` 

## Manager

If you don't want to use the predefined recognizers, use the `Hammer.Manager` instead of `Hammer`. This way you have to set up every touch action event, that you would like to use on your DOM element. This example below shows you, how to set a rotate and a tap event to your element.

```
let cont = divAttr [] []
let manager = Hammer.Manager(cont.Dom)
```

You can use the same methods, that you can use on your Hammer object.

[hammerjs]: http://hammerjs.github.io/
[browsersupp]: http://hammerjs.github.io/browser-support/
[link1]: http://hammerjs.github.io/recognizer-pan/
[link2]: http://hammerjs.github.io/recognizer-pinch/
[link3]: http://hammerjs.github.io/recognizer-press/
[link4]: http://hammerjs.github.io/recognizer-rotate/
[link5]: http://hammerjs.github.io/recognizer-swipe/
[link6]: http://hammerjs.github.io/recognizer-tap/

