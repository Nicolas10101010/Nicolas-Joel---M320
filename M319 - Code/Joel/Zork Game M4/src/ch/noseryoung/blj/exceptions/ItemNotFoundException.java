package ch.noseryoung.blj.exceptions;

public class ItemNotFoundException extends Exception {
    public ItemNotFoundException(String itemName) {
        super("There is no '" + itemName + "' here");
    }
}