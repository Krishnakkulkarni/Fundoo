
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

        string DeleteLabel(int id);

        string UpdateLabels(int id, string newlabel);

        List<LabelsModel> GetLabels(string userId);
    }
}
