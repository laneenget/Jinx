using System.Collections.Generic;

namespace Jinx.Src.Loaders {

    public class AssetManager {

        public List<string> requestedAssets;
        public List<string> loadedAssets;
        public List<string> errorAssets;

        public AssetManager() {

            this.requestedAssets = new List<string>();
            this.loadedAssets = new List<string>();
            this.errorAssets = new List<string>();
        }

        public bool AllAssetsLoaded() {
            return this.requestedAssets.length == (this.loadedAssets.length + this.errorAssets.length);
        }
    }
}