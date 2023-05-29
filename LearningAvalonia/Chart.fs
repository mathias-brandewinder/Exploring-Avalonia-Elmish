namespace LearningAvalonia

[<AutoOpen>]
module PlotView =

    open Avalonia.FuncUI.Builder
    open Avalonia.FuncUI.Types

    open OxyPlot
    open OxyPlot.Avalonia

    let create (attrs: IAttr<PlotView> list): IView<PlotView> =
        ViewBuilder.Create<PlotView>(attrs)

    type PlotView with
        static member model<'T when 'T :> PlotView> (value: PlotModel) : IAttr<'T> =
            AttrBuilder<'T>.CreateProperty<PlotModel>(PlotView.ModelProperty, value, ValueNone)

module Chart =

    open System
    open Elmish
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open OxyPlot
    open OxyPlot.Avalonia
    open OxyPlot.Series

    let createSeries () =
        let rng = Random ()
        Array.init 20 (fun i -> float i, rng.NextDouble ())

    let createPlotModel (series: (float * float) []) =
        let points =
            series
            |> Array.map (fun (x, y) ->
                DataPoint(x, y)
                )

        let series = LineSeries()
        series.StrokeThickness <- 1.0
        series.Color <- OxyColors.Blue
        series.MarkerSize <- 2.0
        series.MarkerStroke <- OxyColors.Blue
        series.MarkerType <- MarkerType.Circle

        points
        |> Array.iter (fun x -> series.Points.Add x)

        let plotModel = PlotModel()
        plotModel.Series.Add series
        plotModel.Title <- "OxyPlot Chart"
        plotModel

    type State = {
        Series: (float * float) []
        }

    let init () : State =
        { Series = createSeries () }

    type Msg =
        | CreateNewSeries

    let update (msg: Msg) (state: State) : State * Cmd<Msg> =
        match msg with
        | CreateNewSeries ->
            { Series = createSeries () },
            Cmd.none

    let view (state: State) (dispatch: Msg -> unit)=
        PlotView.create [
            PlotView.model (state.Series |> createPlotModel)
            ]
