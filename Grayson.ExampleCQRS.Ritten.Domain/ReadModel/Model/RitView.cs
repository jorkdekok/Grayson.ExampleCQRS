using System;

namespace Grayson.ExampleCQRS.Domain.ReadModel.Model
{
    public class RitView
    {
        public int BeginStand { get; set; }
        public Guid BeginStandId { get; set; }
        public int EindStand { get; set; }
        public Guid EindStandId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}