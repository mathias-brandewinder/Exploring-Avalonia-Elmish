namespace LearningAvalonia

module Chart =

    open System
    open Elmish
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL

    let createSeries () =
        let rng = Random ()
        Array.init 20 (fun i -> float i, rng.NextDouble ())

    type State = {
        Series: (float * float) []
        }

    let init () : State =
        { Series = Array.empty }

    type Msg =
        | CreateNewSeries

    let update (msg: Msg) (state: State) : State * Cmd<Msg> =
        match msg with
        | CreateNewSeries ->
            { Series = createSeries () },
            Cmd.none

    let view (state: State) (dispatch: Msg -> unit)=
        TextBlock.create [
            TextBlock.text "TODO"
            ]
