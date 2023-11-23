namespace TARpe22ShopEvert.Models.Spaceship
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? SpaceshipId { get; set; }
    }
}
