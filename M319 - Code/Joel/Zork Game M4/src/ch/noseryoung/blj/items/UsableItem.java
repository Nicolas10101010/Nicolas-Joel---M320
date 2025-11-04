package ch.noseryoung.blj.items;

import ch.noseryoung.blj.core.*;
import ch.noseryoung.blj.exceptions.ItemNotUsableException;
import ch.noseryoung.blj.Game;

// Items that can be activated by the player
public class UsableItem extends Item implements Usable {
    private String usageContext;

    public UsableItem(String name, String description, String usageContext) {
        super(name, description, true);
        this.usageContext = usageContext;
    }

    @Override
    public void use(Player player, Room room, Game game) throws ItemNotUsableException {
        String itemName = this.getName().toLowerCase();

        if (itemName.contains("flashlight")) {
            useFlashlight(game, room);
        } else if (itemName.contains("battery")) {
            useBattery(game, room);
        } else if (itemName.contains("keycard")) {
            useKeycard(game, room);
        } else if (itemName.contains("water")) {
            useWater(room);
        } else {
            throw new ItemNotUsableException(getName());
        }
    }

    private void useFlashlight(Game game, Room room) {
        if (!game.hasFlashlight()) {
            game.setHasFlashlight(true);
            System.out.println("You turn on the flashlight. Its beam cuts through the dim areas.");
            game.revealHiddenItems(room);
        } else {
            System.out.println("The flashlight is already on.");
        }
    }

    private void useBattery(Game game, Room room) throws ItemNotUsableException {
        if (room.getName().equals("Electrical Room") && !game.isPowerFixed()) {
            game.setPowerFixed(true);
            System.out.println("You install the battery. Power restored!");
        } else {
            throw new ItemNotUsableException(getName());
        }
    }

    private void useKeycard(Game game, Room room) throws ItemNotUsableException {
        if (room.getName().equals("Empty Office") || room.getName().equals("Exit")) {
            System.out.println("You insert the " + getName() + " into the card reader.");
            game.incrementKeycardsFound();
            game.checkExitConditions();
        } else {
            throw new ItemNotUsableException(getName());
        }
    }

    private void useWater(Room room) {
        System.out.println("You drink some water. Refreshing!");
        if (room.getName().equals("Wet Carpet Area")) {
            System.out.println("You feel protected from the toxic environment.");
        }
    }

    @Override
    public String getUsageDescription() {
        return "This item can be used in specific contexts: " + usageContext;
    }
}