﻿namespace Beatus.Maui.Models;

public class PredictionDetails
{
    public int Id { get; set; } = 1;
    public byte[] PlantImage { get; set; }
    public string TagName { get; set; }
    public int Probability { get; set; }
    public string Recommendation { get; set; }
}
