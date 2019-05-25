
namespace RepositoryLayer.Interface
{
    using Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILablesRepository
    {
        string AddLabels([FromBody] LabelsModel label);

        string UpdateLabels(LabelsModel label, int id);

        List<LabelsModel> GetLabels(string userId);

        string DeleteLabel(int id);
    }
}
