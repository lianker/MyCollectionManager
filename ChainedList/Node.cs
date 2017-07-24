namespace ChainedLists
{
    public class Node 
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
    }
}
