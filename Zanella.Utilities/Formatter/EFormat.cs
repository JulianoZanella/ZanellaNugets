namespace Zanella.Utilities.Formatter
{
    /// <summary>
    /// Formats
    /// </summary>
    public enum EFormat
    {
        /// <summary>
        /// Fone: <br/>
        /// ####-#### <br/>
        /// (##) ####-#### <br/>
        /// (##) # ####-#### <br/>
        /// +## (##) # ####-#### <br/>
        /// </summary>
        Fone,

        /// <summary>
        /// CPF: ###.###.###-##
        /// </summary>
        CPF,

        /// <summary>
        /// CNPJ: ##.###.###/####-##
        /// </summary>
        CNPJ,

        /// <summary>
        /// CPF: ###.###.###-## <br/>
        /// or CNPJ: ##.###.###/####-##
        /// </summary>
        CPF_CNPJ,
    }
}
