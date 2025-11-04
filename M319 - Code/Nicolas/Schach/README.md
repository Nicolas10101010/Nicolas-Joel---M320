# Chess Application - Competence Assessment M4

## Project Description

### Application Goals

A functional chess application with the following core features:

- Complete chess board with all pieces
- Rule-based movements for all piece types (King, Queen, Rook, Bishop, Knight, Pawn)
- Player switching between White and Black
- Move validation (only legal moves allowed)
- Check detection
- Game state persistence to file (JSON)
- Console-based user interface

### Nice-to-Have Features

- Checkmate detection
- Move history with undo function
- Castling
- En Passant
- Pawn promotion
- AI opponent
- Graphical UI

## Architecture

### Design Patterns Used

#### 1. Factory Pattern

**Purpose**: Creation of chess piece objects  
**Rationale**: The Factory Pattern makes sense because we need to create different piece types (King, Queen, Rook, etc.) without specifying the exact class in the client code. This increases flexibility and maintainability.

**Implementation**: The `PieceFactory` class provides static methods to create pieces based on type and color, and initializes the complete starting position for a chess game.

#### 2. Strategy Pattern

**Purpose**: Movement logic for different piece types  
**Rationale**: Each piece has different movement rules. The Strategy Pattern allows us to encapsulate these rules and swap them at runtime, making the code more modular and extensible.

**Implementation**: Each piece class (King, Queen, Rook, Bishop, Knight, Pawn) implements its own `CanMoveTo()` method that encapsulates the specific movement rules for that piece type.

#### 3. Singleton Pattern

**Purpose**: Game Manager (Game Controller)  
**Rationale**: There should only be one single game instance at a time that manages the game state. This ensures consistent game state management and prevents multiple game instances from conflicting.

**Implementation**: The `GameManager` class uses a thread-safe singleton implementation with a private constructor and a static `Instance` property that ensures only one instance exists throughout the application lifetime.

### Architecture Layers

1. **Presentation Layer**: ConsoleUI - User interaction and display
2. **Business Logic Layer**: GameManager, Board, Pieces, MovementValidator - Game rules and logic
3. **Data Access Layer**: GameRepository - Data persistence and serialization
4. **Model Layer**: Position, Move, Player - Core data structures

### Component Responsibilities

- **GameManager**: Central controller managing game state, player turns, and game flow
- **Board**: Manages the chess board state, piece positions, and captured pieces
- **MovementValidator**: Validates moves according to chess rules and checks for check situations
- **PieceFactory**: Creates and initializes chess pieces
- **ConsoleUI**: Handles all user interaction and board visualization
- **GameRepository**: Persists and loads game state to/from JSON files

## Use Cases

### UC1: Start New Game

**Actor**: Player  
**Description**: Player starts a new chess game  
**Preconditions**: None  
**Postconditions**: Chess board is initialized with standard starting position, White player's turn

**Main Flow**:
1. Player selects "Start new game" from main menu
2. System initializes chess board with standard setup
3. System sets White as current player
4. System displays the board
5. White makes the first move

**Alternative Flows**:
- 1a. Player returns to main menu without starting a game

### UC2: Move Piece

**Actor**: Player  
**Description**: Player moves a piece on the board  
**Preconditions**: Game is active, it's the player's turn  
**Postconditions**: Piece is moved if valid, turn switches to opponent

**Main Flow**:
1. System displays current board state
2. System shows whose turn it is
3. Player enters "move" command
4. System prompts for starting position
5. Player enters starting position (e.g. "e2")
6. System prompts for target position
7. Player enters target position (e.g. "e4")
8. System validates the move
9. System executes the move
10. System checks if opponent's king is in check
11. System switches to next player
12. System displays updated board

**Alternative Flows**:
- 8a. Move is invalid (piece doesn't exist, wrong color, illegal move)
  - System displays error message
  - System returns to step 3
- 10a. Opponent's king is in check
  - System displays "Check!" message
  - System checks for checkmate
- 10b. Checkmate is detected
  - System displays "Checkmate!" message
  - System ends the game

### UC3: Save Game

**Actor**: Player  
**Description**: Player saves the current game state  
**Preconditions**: Game is active  
**Postconditions**: Game state is persisted to JSON file

**Main Flow**:
1. Player selects "Save Game" option
2. System serializes current board position
3. System serializes move history
4. System saves data to JSON file
5. System confirms successful save

**Alternative Flows**:
- 4a. Save fails (file system error)
  - System displays error message
  - Game state remains unchanged

### UC4: Validate Moves

**Actor**: System  
**Description**: System checks if a move is legal according to chess rules  
**Preconditions**: Player has entered a move  
**Postconditions**: Move is validated and either executed or rejected

**Main Flow**:
1. System receives move request (from position, to position)
2. System checks if positions are valid (on board)
3. System checks if piece exists at starting position
4. System checks if piece belongs to current player
5. System checks if target square is reachable according to piece movement rules
6. System checks if path is clear (for pieces that can't jump)
7. System simulates the move
8. System checks if own king would be in check after move
9. System confirms move is valid

**Alternative Flows**:
- 2a-8a. Any validation fails
  - System rejects the move
  - System returns error message

### UC5: Display Game Statistics

**Actor**: Player  
**Description**: Player views current game statistics  
**Preconditions**: Game is active  
**Postconditions**: Statistics are displayed

**Main Flow**:
1. Player enters "stats" command
2. System displays number of moves made
3. System displays current player
4. System displays number of captured pieces
5. Player continues game

### UC6: View Help

**Actor**: Player  
**Description**: Player views game instructions and commands  
**Preconditions**: None  
**Postconditions**: Help information is displayed

**Main Flow**:
1. Player enters "help" command
2. System displays piece symbols and meanings
3. System displays available commands
4. System displays move notation explanation
5. Player returns to game

## Technical Details

### Technologies Used

- **Language**: C# (.NET 9.0)
- **Persistence**: JSON serialization
- **Interface**: Console application
- **IDE**: JetBrains Rider / Visual Studio

### Class Overview

1. **Program** - Application entry point
2. **GameManager** - Game controller (Singleton pattern)
3. **Board** - Chess board management and state
4. **Piece (abstract)** - Base class for all chess pieces
5. **King, Queen, Rook, Bishop, Knight, Pawn** - Specific piece implementations
6. **PieceFactory** - Factory for piece creation
7. **MovementValidator** - Validates moves and checks for check situations
8. **ConsoleUI** - User interface and board visualization
9. **IGameRepository** - Interface for data persistence
10. **JsonGameRepository** - JSON-based implementation of game persistence
11. **Position** - Represents a board position with row/column and chess notation
12. **Move** - Represents a move with from/to positions and timestamp
13. **Player** - Represents a player with color and name
14. **GameState** - Data structure for game serialization
15. **PieceData** - Data structure for piece serialization
16. **MoveData** - Data structure for move serialization
17. **PieceType** - Enum for piece types
18. **PlayerColor** - Enum for player colors

### Inheritance Hierarchy

```
Piece (abstract)
├── King
├── Queen
├── Rook
├── Bishop
├── Knight
└── Pawn
```

**Rationale for Inheritance**: All chess pieces share common properties (position, color, type, hasMovedFlag) and behaviors (movement, display), but have different movement rules. This is a classic case for inheritance with polymorphism where the base class defines the contract and common functionality, while derived classes implement piece-specific movement logic.

### Interfaces

- **IGameRepository**: Defines methods for data persistence (SaveGame, LoadGame, GameExists)
  - Allows for different persistence implementations (JSON, XML, database)
  - Currently implemented by JsonGameRepository

### Key Design Decisions

1. **Position Representation**: Uses both array indices (row, column) and chess notation (e.g., "e4") for flexibility
2. **Move Validation**: Separated into MovementValidator class for single responsibility
3. **Check Detection**: Simulates moves to detect if king would be in check
4. **State Management**: GameManager maintains game state and enforces turn-based play
5. **Error Handling**: Try-catch blocks throughout for robust error management

## Clean Code Examples

The following Clean Code principles are applied in the code:

### Descriptive Naming
```csharp
// Clear, intention-revealing names
public bool IsKingInCheck(PlayerColor color)
public Piece? GetPieceAt(Position position)
public bool MovePiece(Position from, Position to, PlayerColor currentPlayer)
```

### Single Responsibility Principle (SRP)
- **Board**: Manages board state only
- **MovementValidator**: Only validates moves
- **GameManager**: Only manages game flow
- **ConsoleUI**: Only handles user interaction

### DRY (Don't Repeat Yourself)
- Piece creation logic centralized in PieceFactory
- Move validation logic in one place (MovementValidator)
- Check detection reused in multiple contexts

### Small, Focused Methods
```csharp
private void SwitchPlayer()
{
    CurrentPlayer = CurrentPlayer.Color == PlayerColor.White 
        ? _blackPlayer 
        : _whitePlayer;
}
```

### Meaningful Comments
- XML documentation for public methods
- Comments only where logic needs clarification
- Self-documenting code preferred over excessive comments

### Consistent Formatting
- Consistent indentation and spacing
- Logical grouping of related methods
- Clear separation of concerns

## Development Process

### AI Support Documentation

#### Architecture Design Prompts

**Initial Structure Prompt**:
```
"I'm building a chess application in C#. I need to implement:
- Complete chess board with all pieces
- Movement rules for all piece types
- Player switching
- Move validation
- Check detection
- Game state persistence to JSON

Suggest an architecture using appropriate design patterns."
```

**Result**: GitHub Copilot suggested Factory pattern for piece creation, Singleton for GameManager, and interface-based repository pattern for persistence.

#### Class Diagram Generation Prompts

**Prompt for UML Class Diagram**:
```
"Based on my chess application code, generate a UML class diagram showing:
- All classes and their relationships
- Design patterns used (Factory, Singleton)
- Inheritance hierarchy for chess pieces
- Interfaces for repository pattern
Include associations, dependencies, and multiplicities."
```

**Prompt for Sequence Diagram**:
```
"Create a sequence diagram for the 'Move Piece' use case showing:
- Player interaction with ConsoleUI
- ConsoleUI calling GameManager
- GameManager coordinating with Board
- Board using MovementValidator
- Validation checking Piece movement rules
- Check detection after move
Include both successful move and invalid move scenarios."
```

### AI-Generated Code Sections

The following sections were initially generated with GitHub Copilot assistance:

1. **PieceFactory.CreateStartingPieces()**: Boilerplate code for initial board setup
2. **Position class**: Conversion between chess notation and array indices
3. **JSON serialization structures**: GameState, PieceData, MoveData classes
4. **ConsoleUI board display**: ASCII art and color formatting

**Note**: All AI-generated code was reviewed, tested, and modified as needed to fit the project requirements.

### Commits

All development steps are documented in individual, traceable commits with clear commit messages following conventional commit format:
- `feat:` for new features
- `fix:` for bug fixes
- `refactor:` for code improvements
- `docs:` for documentation updates

## Diagrams

The UML Class Diagram and Sequence Diagram are provided as separate Mermaid diagram files.

## Implemented Features

### Must-Have Criteria (All Implemented ✓)

- [x] Complete chess board with all pieces (8x8 board with proper initialization)
- [x] Rule-based movements for all piece types (King, Queen, Rook, Bishop, Knight, Pawn with correct movement patterns)
- [x] Player switching between White and Black (automatic turn management)
- [x] Move validation (comprehensive validation including piece ownership, valid destinations, clear paths)
- [x] Check detection (real-time check detection with move simulation)
- [x] Game state persistence (JSON repository implemented with save/load functionality)
- [x] Console-based user interface (clean, colored board display with intuitive commands)

### Partially Implemented (Nice-to-have)

- [x] **Checkmate detection** - Fully implemented with move availability checking
- [x] **Move history** - Timestamped move tracking (undo not implemented)
- [ ] **Castling** - Not implemented (planned for future version)
- [ ] **En Passant** - Not implemented (planned for future version)
- [ ] **Pawn promotion** - Not implemented (planned for future version)
- [ ] **AI opponent** - Not implemented (would require separate AI logic component)
- [ ] **Graphical UI** - Not implemented (console-only at this stage)

### Feature Details

**Move Validation Includes**:
- Piece-specific movement patterns
- Path obstruction checking (for non-jumping pieces)
- Capture validation (can't capture own pieces)
- Check prevention (can't move into check)
- Turn enforcement (only current player can move)

**Check Detection Features**:
- Real-time check detection after each move
- Visual indication when king is in check
- Move simulation to prevent illegal moves
- Checkmate detection with game-ending logic

## Custom Classes Used

1. **Position** - Board position representation with chess notation
2. **Move** - Move representation with timestamp
3. **Player** - Player data with color and name
4. **Piece (abstract)** - Base class for all chess pieces
5. **King** - King piece with specific movement rules
6. **Queen** - Queen piece with specific movement rules
7. **Rook** - Rook piece with specific movement rules
8. **Bishop** - Bishop piece with specific movement rules
9. **Knight** - Knight piece with specific movement rules
10. **Pawn** - Pawn piece with specific movement rules
11. **PieceFactory** - Factory for creating chess pieces
12. **Board** - Chess board management
13. **MovementValidator** - Move validation logic
14. **GameManager** - Game state and flow controller (Singleton)
15. **ConsoleUI** - User interface and display
16. **IGameRepository** - Repository interface
17. **JsonGameRepository** - JSON persistence implementation
18. **GameState** - Serializable game state
19. **PieceData** - Serializable piece data
20. **MoveData** - Serializable move data

**Total: 20 Classes/Interfaces** (exceeds requirement of min. 8 classes)

## Code Statistics

- **Total Lines of Code**: ~1,385 lines
- **Classes**: 20 (including interfaces and data structures)
- **Design Patterns**: 3 (Factory, Singleton, Repository)
- **Interfaces**: 1 (IGameRepository)
- **Enums**: 2 (PieceType, PlayerColor)
- **Inheritance Hierarchy**: Piece with 6 subclasses
- **Namespaces**: 5 (Models, Models.Pieces, Logic, UI, Data, Factories)

## Running Instructions

### Prerequisites
- .NET 9.0 SDK installed
- Terminal/Console application

### Running the Application

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd Schach
   ```

2. **Build the Project**
   ```bash
   dotnet build
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

4. **Start Playing**
   - Select "1" to start a new game
   - Use "move" command to make moves
   - Enter moves using chess notation (e.g., "e2" to "e4")

### Game Commands

**In-Game Commands**:
- `move` - Execute a move (will prompt for from/to positions)
- `help` - Display help information about pieces and commands
- `stats` - Show current game statistics
- `menu` - Return to main menu (warns about unsaved progress)

**Movement Notation**:
- Use lowercase letters for files (columns): a-h
- Use numbers for ranks (rows): 1-8
- Examples: e2, e4, d7, d5

## Testing the Application

### Test Scenarios

1. **Basic Movement**
   - Move pawn: e2 to e4
   - Move knight: g1 to f3

2. **Invalid Moves**
   - Try to move opponent's piece
   - Try to move into occupied square (same color)
   - Try to move through pieces (non-knight)

3. **Check Situations**
   - Create check situation
   - Verify check is detected
   - Verify invalid moves that don't resolve check

4. **Checkmate**
   - Scholar's Mate: e2-e4, e7-e5, Bc1-c4, Nb8-c6, Qd1-h5, Ng8-f6, Qh5xf7# (checkmate)

## Conclusion

### Learning Process

**What Went Well**:
- Structuring the project into clear, logical layers (Models, Logic, UI, Data, Factories)
- Successfully implementing three design patterns to solve real architectural problems
- Creating a robust move validation system with check detection
- Exception handling throughout for reliable error management
- Clear separation of concerns with single responsibility for each class
- Achieving all must-have criteria with additional nice-to-have features

**Challenges**:
- Implementing complex chess rules, especially pawn movement (first move, two squares forward)
- Check detection with move simulation (undo mechanism for validation)
- Structuring and maintaining a larger codebase with multiple interacting components
- Balancing between perfect OOP design and practical implementation
- Ensuring thread-safety in the Singleton pattern

**AI Support**:
- GitHub Copilot helped significantly with boilerplate code and structural suggestions
- AI generated initial code for JSON serialization structures
- Architectural decisions were made independently after reviewing AI suggestions
- Core game logic was developed and understood independently
- All AI-generated code was reviewed, tested, and adapted to fit specific requirements

### Concepts Learned

**Object-Oriented Programming**:
- Inheritance and polymorphism in practice (Piece hierarchy)
- Abstract classes for shared behavior with specialized implementations
- Encapsulation through access modifiers and property protection
- Composition over inheritance (Board has-a MovementValidator)

**Design Patterns**:
- **Factory Pattern**: Centralized object creation, reduced coupling, easy to extend
- **Singleton Pattern**: Controlled single instance, global access point, thread-safe implementation
- **Repository Pattern**: Abstraction of data access, easy to swap implementations

**Software Design Principles**:
- Single Responsibility Principle (SRP) - each class has one clear purpose
- Dependency Inversion - depend on abstractions (IGameRepository)
- Interface-based design for flexibility and testability
- Separation of concerns across architectural layers

**Best Practices**:
- XML documentation for public APIs
- Exception handling and error recovery
- Input validation and user feedback
- Consistent code formatting and naming conventions
- Git commit best practices

### Future Improvements

**Technical Enhancements**:
1. Implement remaining chess rules (castling, en passant, pawn promotion)
2. Add undo/redo functionality using Command pattern
3. Implement AI opponent using Minimax algorithm
4. Add unit tests for core game logic
5. Create graphical UI using WPF or Avalonia

**Code Quality**:
1. Refactor long methods in ConsoleUI
2. Add more comprehensive error handling
3. Improve save game format to include more metadata
4. Optimize check detection algorithm
5. Add logging for debugging

### Final Assessment

The project successfully demonstrates all required OOP concepts and design patterns in a real, functional chess application. The architecture is clean, maintainable, and extensible. The game is fully playable with proper rule enforcement, making it a solid foundation for future enhancements.

**Key Achievements**:
- 20 custom classes showing deep understanding of OOP
- 3 design patterns properly implemented and justified
- Complete chess game with most core rules
- Clean, documented, and maintainable code
- Successful use of AI tools while maintaining understanding

This project serves as a comprehensive demonstration of object-oriented design principles, architectural patterns, and practical C# programming skills.