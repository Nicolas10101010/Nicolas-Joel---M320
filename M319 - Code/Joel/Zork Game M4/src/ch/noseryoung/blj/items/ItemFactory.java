package ch.noseryoung.blj.items;

// Factory Pattern: centralizes item creation
public class ItemFactory {

    public static Item createUsableItem(String name, String description, String usageContext) {
        return new UsableItem(name, description, usageContext);
    }

    public static Item createSceneryItem(String name, String description) {
        return new SceneryItem(name, description);
    }
}