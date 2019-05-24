
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

        string DeleteLabel(int id);

        List<LabelsModel> GetLabels(string userId);

        string UpdateLabels(int id, string newlabel);
    }
}
