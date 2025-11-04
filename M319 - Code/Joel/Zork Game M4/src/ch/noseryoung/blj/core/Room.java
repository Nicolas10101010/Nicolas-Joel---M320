package ch.noseryoung.blj.core;

import ch.noseryoung.blj.items.Item;
import java.util.ArrayList;

public class Room {
    private String name;
    private String description;
    private Room northRoom;
    private Room southRoom;
    private Room eastRoom;
    private Room westRoom;
    private ArrayList<Item> items;

    public Room(String name, String description, Room northRoom, Room southRoom, Room eastRoom, Room westRoom) {
        this.name = name;
        this.description = description;
        this.northRoom = northRoom;
        this.southRoom = southRoom;
        this.eastRoom = eastRoom;
        this.westRoom = westRoom;
        this.items = new ArrayList<>();
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

    public Room getNorthRoom() {
        return northRoom;
    }

    public void setNorthRoom(Room northRoom) {
        this.northRoom = northRoom;
    }

    public Room getSouthRoom() {
        return southRoom;
    }

    public void setSouthRoom(Room southRoom) {
        this.southRoom = southRoom;
    }

    public Room getEastRoom() {
        return eastRoom;
    }

    public void setEastRoom(Room eastRoom) {
        this.eastRoom = eastRoom;
    }

    public Room getWestRoom() {
        return westRoom;
    }

    public void setWestRoom(Room westRoom) {
        this.westRoom = westRoom;
    }

    public void addItem(Item item) {
        items.add(item);
    }

    public boolean removeItem(Item item) {
        return items.remove(item);
    }

    public Item getItemByName(String itemName) {
        for (Item item : items) {
            if (item.getName().equalsIgnoreCase(itemName)) {
                return item;
            }
        }
        return null;
    }

    public ArrayList<Item> getItems() {
        return items;
    }

    public boolean hasItem(String itemName) {
        return getItemByName(itemName) != null;
    }
}