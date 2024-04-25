namespace Domain.Implements
{
    using Domain.Dacs;
    using Domain.Interfaces;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ParameterDac : BaseDac, IParameterDac
    {
        public ParameterDac(IApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public IEnumerable<SelectListItem> QueryParametersByType(string typeName)
        {
            string sqlStr = $@"
                SELECT 
                    NAME AS Text, VALUE AS Value
                FROM PARAMETER
                WHERE TYPE = @typeName";
            return base.ExcuteQuery<SelectListItem>
                                    (sqlStr, new { typeName }).AsEnumerable();

        }
    }
}
