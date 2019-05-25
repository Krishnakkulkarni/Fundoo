
namespace BussinessLayer.Interfaces
{
    using Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILabels
    {
        string AddLabels(LabelsModel label);
        
        List<LabelsModel> GetLabels(string userId);

        string UpdateLabels(LabelsModel label,int id);

        string DeleteLabel(int id);
    }
}
