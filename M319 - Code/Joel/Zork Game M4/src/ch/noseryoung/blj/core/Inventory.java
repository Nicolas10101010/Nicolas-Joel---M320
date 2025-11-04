package ch.noseryoung.blj.core;

import ch.noseryoung.blj.items.Item;
import ch.noseryoung.blj.exceptions.InventoryFullException;
import java.util.ArrayList;
import java.util.List;

// Generic container for items with capacity limit
public class Inventory<T> {
    private List<T> items;
    private int maxCapacity;

    public Inventory(int maxCapacity) {
        this.maxCapacity = maxCapacity;
        this.items = new ArrayList<>();
    }

    public boolean addItem(T item) throws InventoryFullException {
        if (items.size() >= maxCapacity) {
            throw new InventoryFullException();
        }
        items.add(item);
        return true;
    }

    public boolean removeItem(T item) {
        return items.remove(item);
    }

    public boolean hasItem(String itemName) {
        for (T item : items) {
            if (item instanceof Item) {
                if (((Item) item).getName().equalsIgnoreCase(itemName)) {
                    return true;
                }
            }
        }
        return false;
    }

    public void showInventory() {
        if (items.isEmpty()) {
            System.out.println("Your inventory is empty.");
        } else {
            System.out.println("Inventory:");
            for (T item : items) {
                if (item instanceof Item) {
                    System.out.println("- " + ((Item) item).getName());
                }
            }
        }
    }

    public int getSize() {
        return items.size();
    }

    public int getMaxCapacity() {
        return maxCapacity;
    }

    public List<T> getItems() {
        return new ArrayList<>(items);
    }
}