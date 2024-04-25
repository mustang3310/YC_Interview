namespace Domain
{
    public static class SqlOperatorsExtension
    {
        /// <summary>
        /// like
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddWildcards(this string str)
            => $"%{str}%";

    }
}
