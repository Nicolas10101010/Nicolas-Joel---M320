package ch.noseryoung.blj.exceptions;

public class InventoryFullException extends Exception {
    public InventoryFullException() {
        super("Your inventory is full! Drop something first");
    }
}