namespace Api.Models
{
    public class RequestModel
    {
        /// <summary>
        /// Удалить предлоги
        /// </summary>
        public bool RemoveGrammars { get; set; }

        /// <summary>
        /// Адрес ресурса
        /// </summary>
        public string? Uri { get; set; }

        /// <summary>
        /// Размер группы слов
        /// </summary>
        public int GroupsCount { get; set; }
    }
}
