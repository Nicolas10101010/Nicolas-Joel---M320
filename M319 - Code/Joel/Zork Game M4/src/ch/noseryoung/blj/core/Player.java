package ch.noseryoung.blj.core;

import ch.noseryoung.blj.items.Item;
import ch.noseryoung.blj.exceptions.InventoryFullException;

public class Player {
    private static final int DEFAULT_INVENTORY_SIZE = 10;

    private String name;
    private Room currentRoom;
    private int health;
    private Inventory<Item> inventory;

    public Player(String name, Room currentRoom, int health) {
        this.name = name;
        this.currentRoom = currentRoom;
        this.health = health;
        this.inventory = new Inventory<>(DEFAULT_INVENTORY_SIZE);
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Room getCurrentRoom() {
        return currentRoom;
    }

    public void setCurrentRoom(Room currentRoom) {
        this.currentRoom = currentRoom;
    }

    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public Inventory<Item> getInventory() {
        return inventory;
    }

    public boolean addItemToInventory(Item item) throws InventoryFullException {
        return inventory.addItem(item);
    }

    public boolean removeItemFromInventory(Item item) {
        return inventory.removeItem(item);
    }

    public boolean hasItem(String itemName) {
        return inventory.hasItem(itemName);
    }
}