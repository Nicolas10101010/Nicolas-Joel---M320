package ch.noseryoung.blj.items;

import ch.noseryoung.blj.Game;
import ch.noseryoung.blj.core.Player;
import ch.noseryoung.blj.core.Room;
import ch.noseryoung.blj.exceptions.ItemNotUsableException;

public interface Usable {
    void use(Player player, Room room, Game game) throws ItemNotUsableException;
    String getUsageDescription();
}