# ChessGame

ChessGame is a simple C# WPF application that allows two users to play a game of chess with a visual board interface. It is structured using an object-oriented approach with extensibility and maintainability in mind.

## Features

- Complete chess board UI with 8x8 grid using WPF
- All major chess pieces implemented with valid move logic
- Highlighting of selectable moves
- Player turn switching and visual indication
- Save and load board state as XML
- Custom test boards for debugging
- New Game functionality to reset the board

## Technologies Used

- C# (.NET)
- WPF (Windows Presentation Foundation)
- MVVM-inspired component separation
- XML Serialization

## Project Structure

- `ChessControl.xaml` – Custom user control displaying the chess board.
- `Board.cs` – Manages the game state, pieces, and move logic.
- `Coordinate.cs` – Encapsulates position logic and transformations.
- `Pieces/` – Contains all individual piece classes inheriting from `Piece.cs`.
- `MainWindow.xaml` – Entry point with buttons for save/load/reset.
- `README.md` – Project documentation.

## Running the Project

1. Clone the repository
2. Open the solution in Visual Studio
3. Set `ChessGame` as the startup project
4. Run the application

## Saving and Loading

- Click **Save Game** to export the current state to an XML file.
- Click **Load Game** to import a saved game from an XML file.

XML Format Example:

```xml
<Board OnTurn="White">
  <allFields>
    <Pawn color="White" z="2" c="A" />
    <King color="Black" z="8" c="E" />
    ...
  </allFields>
</Board>
```

## Authors

Created by Abhishek Sagar as part of coursework in Advanced Appication Programming.

## License

This project is open-source and free to use under the MIT License.

## Limitations

- The game does not detect or enforce checkmate, stalemate, or draw conditions.
- Special moves like castling, en passant, and pawn promotion are not implemented.
