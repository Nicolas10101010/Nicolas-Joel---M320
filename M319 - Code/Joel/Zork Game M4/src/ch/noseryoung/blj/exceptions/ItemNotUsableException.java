package ch.noseryoung.blj.exceptions;

public class ItemNotUsableException extends Exception {
    public ItemNotUsableException(String itemName) {
        super("You can't use the '" + itemName + "' here");
    }
}