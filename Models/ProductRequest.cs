using System;

namespace SponteAPI.Models
{
  public class ProductRequest
  {

    public string Title { get; set; }

    public string CompleteDescription { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public double Length { get; set; }

    public double Weight { get; set; }

    public int BarCode { get; set; }

    public double Value { get; set; }

    public DateTime AcquisitionDate { get; set; }

    public string ProductImageLink { get; set; }
  }
}