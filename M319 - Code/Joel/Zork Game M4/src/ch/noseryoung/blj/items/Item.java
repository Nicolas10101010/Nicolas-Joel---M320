package ch.noseryoung.blj.items;

public abstract class Item {
    private String name;
    private String description;
    private boolean canBeTaken;
    private boolean isHidden = false;


    public Item(String name, String description, boolean canBeTaken) {
        this.name = name;
        this.description = description;
        this.canBeTaken = canBeTaken;
    }

    public String getName() {
        return name;
    }

    public String getDescription() {
        return description;
    }

    public void setName(String newName) {
        this.name = newName;
    }

    public void setDescription(String newDescription) {
        this.description = newDescription;
    }

    public boolean canBeTaken() {
        return canBeTaken;
    }

    public void setCanBeTaken(boolean canBeTaken) {
        this.canBeTaken = canBeTaken;
    }

    public boolean isHidden() {
        return isHidden;
    }
    public void setHidden(boolean hidden) {
        this.isHidden = hidden;
    }
}