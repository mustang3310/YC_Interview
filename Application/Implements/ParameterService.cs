namespace Application.Implements
{
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class ParameterService : BaseService, IParameterService
    {
        private readonly IParameterDac parameterDac;

        public ParameterService(IParameterDac parameterDac)
            => this.parameterDac = parameterDac;

        public IEnumerable<SelectListItem> QueryParametersByType(string typeName)
            => parameterDac.QueryParametersByType(typeName);
    }
}
