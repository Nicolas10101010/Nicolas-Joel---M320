package ch.noseryoung.blj;

import ch.noseryoung.blj.core.*;
import ch.noseryoung.blj.items.*;
import ch.noseryoung.blj.exceptions.*;
import ch.noseryoung.blj.setup.*;
import java.util.Scanner;

public class Game {
    // Clean Code: Constants instead of magic numbers
    private static final int REQUIRED_KEYCARDS = 3;
    private static final int PLAYER_STARTING_HEALTH = 100;

    private Player player;
    private boolean gameRunning;
    private boolean powerFixed = false;
    private boolean hasFlashlight = false;
    private int keycardsFound = 0;

    public Game() {
        setupGame();
        gameRunning = true;
    }

    private void setupGame() {
        Room[] rooms = WorldBuilder.createWorld();
        ItemPopulator.populateWorld(rooms);
        this.player = new Player("Player", rooms[0], PLAYER_STARTING_HEALTH);
    }

    public void startGame() {
        showCurrentRoom();
        while(gameRunning) {
            processInput();
        }
    }

    private void processInput() {
        Scanner scanner = new Scanner(System.in);
        System.out.print("\n> ");
        String input = scanner.nextLine();
        processCommand(input);
    }

    private void showCurrentRoom() {
        System.out.println(player.getCurrentRoom().getDescription());
    }

    private void processCommand(String input) {
        String[] words = input.toLowerCase().trim().split(" ");
        if (words.length == 0) return;

        String command = words[0];

        switch (command) {
            case "quit": case "exit":
                quitGame();
                break;
            case "go":
                handleGoCommand(words);
                break;
            case "north": case "n":
            case "south": case "s":
            case "east": case "e":
            case "west": case "w":
                movePlayer(command);
                break;
            case "look":
                showItemsInRoom();
                break;
            case "examine": case "inspect":
                handleExamineCommand(words);
                break;
            case "inventory": case "inv": case "i":
                player.getInventory().showInventory();
                break;
            case "take":
                handleTakeCommand(words);
                break;
            case "use":
                handleUseCommand(words);
                break;
            case "help":
                showHelpMenu();
                break;
            default:
                System.out.println("Invalid input");
        }
    }

    private void quitGame() {
        gameRunning = false;
        System.out.println("Turning off...");
    }

    private void handleGoCommand(String[] words) {
        if (words.length < 2) {
            System.out.println("Give the direction");
        } else {
            movePlayer(words[1]);
        }
    }

    private void handleExamineCommand(String[] words) {
        if (words.length < 2) {
            System.out.println("Which object?");
        } else {
            examineItem(words[1]);
        }
    }

    private void handleTakeCommand(String[] words) {
        if (words.length < 2) {
            System.out.println("Which object?");
        } else {
            takeItem(words[1]);
        }
    }

    private void handleUseCommand(String[] words) {
        if (words.length < 2) {
            System.out.println("Which item?");
        } else {
            useItem(words[1]);
        }
    }

    private void showHelpMenu() {
        System.out.println("=== AVAILABLE COMMANDS ===");
        System.out.println("Movement:");
        System.out.println("  go north (n) - Move north");
        System.out.println("  go south (s) - Move south");
        System.out.println("  go east (e)  - Move east");
        System.out.println("  go west (w)  - Move west");
        System.out.println("Actions:");
        System.out.println("  look         - Look around");
        System.out.println("  take [item]  - Pick up item");
        System.out.println("  examine [item] - Inspect item");
        System.out.println("  inventory (i)  - Show inventory");
        System.out.println("  use [item]     - Use item");
        System.out.println("  quit/exit      - Exit game");
    }

    private void movePlayer(String direction) {
        try {
            Room nextRoom = getNextRoom(direction);
            if (nextRoom == null) {
                throw new InvalidDirectionException(direction);
            }
            player.setCurrentRoom(nextRoom);
            showCurrentRoom();
        } catch (InvalidDirectionException e) {
            System.out.println(e.getMessage());
        }
    }

    private Room getNextRoom(String direction) {
        Room current = player.getCurrentRoom();
        switch (direction) {
            case "north": case "n": return current.getNorthRoom();
            case "south": case "s": return current.getSouthRoom();
            case "east": case "e": return current.getEastRoom();
            case "west": case "w": return current.getWestRoom();
            default: return null;
        }
    }

    private void takeItem(String itemName) {
        try {
            Room room = player.getCurrentRoom();
            Item item = room.getItemByName(itemName);

            if (item == null) throw new ItemNotFoundException(itemName);
            if (!item.canBeTaken()) {
                System.out.println("You can't take the " + item.getName() + ".");
                return;
            }

            if (player.addItemToInventory(item)) {
                room.removeItem(item);
                System.out.println("You take the " + item.getName() + ".");
            }
        } catch (ItemNotFoundException | InventoryFullException e) {
            System.out.println(e.getMessage());
        }
    }

    private void examineItem(String itemName) {
        try {
            Item item = player.getCurrentRoom().getItemByName(itemName);
            if (item == null) throw new ItemNotFoundException(itemName);

            System.out.println(item.getDescription());

            if (item.canBeTaken()) {
                offerToTakeItem(item);
            }
        } catch (ItemNotFoundException e) {
            System.out.println(e.getMessage());
        }
    }

    private void offerToTakeItem(Item item) {
        System.out.print("Do you want to take it? (y/n): ");
        Scanner scanner = new Scanner(System.in);
        String answer = scanner.nextLine().toLowerCase();

        if (answer.equals("y") || answer.equals("yes")) {
            try {
                if (player.addItemToInventory(item)) {
                    player.getCurrentRoom().removeItem(item);
                    System.out.println("You take the " + item.getName() + ".");
                }
            } catch (InventoryFullException e) {
                System.out.println(e.getMessage());
            }
        }
    }

    private void showItemsInRoom() {
        System.out.println("You see:");
        boolean foundItems = false;

        for (Item item : player.getCurrentRoom().getItems()) {
            if (!item.isHidden() || hasFlashlight) {
                System.out.println("> " + item.getName());
                foundItems = true;
            }
        }

        if (!foundItems) {
            System.out.println("Nothing of interest in the dim light.");
        }
    }

    private void useItem(String itemName) {
        if (!player.hasItem(itemName)) {
            System.out.println("You don't have a " + itemName + ".");
            return;
        }

        Item item = findItemInInventory(itemName);
        if (item == null) return;

        // Polymorphism: different items behave differently
        if (item instanceof Usable) {
            try {
                ((Usable) item).use(player, player.getCurrentRoom(), this);
            } catch (ItemNotUsableException e) {
                System.out.println(e.getMessage());
            }
        } else {
            System.out.println("You can't use the " + itemName + ".");
        }
    }

    private Item findItemInInventory(String itemName) {
        for (Item item : player.getInventory().getItems()) {
            if (item.getName().equalsIgnoreCase(itemName)) {
                return item;
            }
        }
        return null;
    }

    // Helper methods for item behavior
    public boolean hasFlashlight() { return hasFlashlight; }
    public void setHasFlashlight(boolean value) { this.hasFlashlight = value; }
    public boolean isPowerFixed() { return powerFixed; }
    public void setPowerFixed(boolean value) { this.powerFixed = value; }
    public void incrementKeycardsFound() { this.keycardsFound++; }

    public void checkExitConditions() {
        if (keycardsFound >= REQUIRED_KEYCARDS && powerFixed) {
            System.out.println("All keycards inserted and power restored!");
            System.out.println("The exit door opens...");
            System.out.println("\n=== CONGRATULATIONS! YOU ESCAPED! ===");
            gameRunning = false;
        } else if (keycardsFound >= REQUIRED_KEYCARDS) {
            System.out.println("All keycards inserted, but power is missing.");
        } else {
            System.out.println("Progress: " + keycardsFound + "/" + REQUIRED_KEYCARDS + " keycards.");
        }
    }

    public void revealHiddenItems(Room room) {
        for (Item item : room.getItems()) {
            if (item.isHidden()) {
                item.setHidden(false);
                System.out.println("You found a hidden " + item.getName() + "!");
            }
        }
    }
}