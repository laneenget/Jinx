namespace Jinx.Src.Loaders {

    public class AssetManager {

        public string[] requestedAssets;
        public string[] loadedAssets;
        public string[] errorAssets;

        public AssetManager() {

            this.requestedAssets = new string[0];
            this.loadedAssets = new string[0];
            this.errorAssets = new string[0];
        }

        public bool AllAssetsLoaded() {
            return this.requestedAssets.length == (this.loadedAssets.length + this.errorAssets.length);
        }
    }
}