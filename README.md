# Perceptron Line Fitting

I built a small Windows Forms experiment for entering points and exploring parameter fitting with hill climbing. It uses a weighted sum and bias, mutates one parameter at a time, and keeps a mutation when it lowers mean squared error.

## Original demo

https://github.com/user-attachments/assets/57c4a147-fd0c-4bd0-be41-cf7b6879e28f

## What the current code does

- I enter integer X/Y pairs in the form and add them to the plot.
- Each submission resets the parameters and runs **100,000 hill-climbing iterations** over the accumulated points.
- `Perceptron.cs` computes weighted sums and accepts or rejects random mutations.
- `Form1.cs` draws the points, axes, and a line derived from the weights.

The repository’s earlier caption referred to a sine wave. The checked-in form currently fits manually entered points with a linear model; it does not implement a nonlinear sine-wave approximator.

## Run locally

This is a **Windows Forms / .NET Framework 4.7.2** project. Use Windows and Visual Studio with .NET desktop development support and the 4.7.2 targeting pack. Open `Perceptron Line of Best Fit.sln`, select the form application, and run it.

The solution is not a cross-platform .NET console app. A local `dotnet build` attempt on macOS stopped because the .NET Framework reference assemblies were missing; the UI was not rerun.

## A limitation worth knowing

The training input contains one feature, while the rendered line uses two weights. That mismatch needs fixing before I can present the drawing as a faithful view of the fitted model. The form also performs training on the UI thread and expects valid integer input.

## Code guide

| File in `Perceptron Line of Best Fit/` | Responsibility |
| --- | --- |
| `Perceptron.cs` | Prediction, error, mutation, hill climbing |
| `Form1.cs` | Point entry, training, plotting |
| `doublePoint.cs` | Point coordinates |
