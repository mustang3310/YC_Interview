namespace Application.Services
{
    using Domain.Dacs;
    using System;

    public class DataInitialService
    {
        private readonly BaseDac baseDac;
        public DataInitialService(BaseDac baseDac)
        {
            this.baseDac = baseDac;
        }

        public bool Excute(string sql)
        {
            return this.baseDac.Excute(sql);
        }
    }
}
