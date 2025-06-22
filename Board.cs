using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ChessGame
{
    public class Board : IXmlSerializable
    {
        public Piece[,] fields = new Piece[8, 8];

        public ChessColor onTurn = ChessColor.White;

        public ChessColor OnTurn
        {
            get { return onTurn; }
            set { onTurn = value; }
        }

        public bool Move(Coordinate source, Coordinate destination)
        {
            Piece piece = fields[source.X, source.Y];

            if (piece == null)
                return false;
            if (piece.Color != onTurn)
                return false;
            Coordinate[] legalMoves = piece.PossibleMoves(this, source);
            if (legalMoves.Contains(destination))
                return PerformMove(source, destination);
            else return false;
        }

        public bool PerformMove(Coordinate from, Coordinate to)
        {
            fields[to.X, to.Y] = fields[from.X, from.Y];
            fields[from.X, from.Y] = null;
            if (onTurn == ChessColor.White)
                onTurn = ChessColor.Black;
            else
                onTurn = ChessColor.White;
            return true;
        }

        public static Board standardBoard()
        {
            Board board = new Board();

            //Pawns
            for (int i = 0; i < 8; i++)
            {
                board.fields[i, 1] = new Pawn(ChessColor.White);
                board.fields[i, 6] = new Pawn(ChessColor.Black);
            }

            // Rooks
            board.fields[0, 0] = new Rook(ChessColor.White);
            board.fields[7, 0] = new Rook(ChessColor.White);
            board.fields[0, 7] = new Rook(ChessColor.Black);
            board.fields[7, 7] = new Rook(ChessColor.Black);

            //Knights
            board.fields[1, 0] = new Knight(ChessColor.White);
            board.fields[6, 0] = new Knight(ChessColor.White);
            board.fields[1, 7] = new Knight(ChessColor.Black);
            board.fields[6, 7] = new Knight(ChessColor.Black);

            //Bishops
            board.fields[2, 0] = new Bishop(ChessColor.White);
            board.fields[5, 0] = new Bishop(ChessColor.White);
            board.fields[2, 7] = new Bishop(ChessColor.Black);
            board.fields[5, 7] = new Bishop(ChessColor.Black);

            //Queens
            board.fields[3, 0] = new Queen(ChessColor.White);
            board.fields[3, 7] = new Queen(ChessColor.Black);

            //Kings
            board.fields[4, 0] = new King(ChessColor.White);
            board.fields[4, 7] = new King(ChessColor.Black);

            return board;
        }

        public static Board TestBoard1()
        {
            Board board = new Board();

            board.fields[4, 4] = new King(ChessColor.White); // E5

            board.fields[5, 6] = new Pawn(ChessColor.White); // F7
            board.fields[3, 3] = new Pawn(ChessColor.Black); // D4
            return board;
        }

        public static Board TestBoard2()
        {
            Board board = new Board();

            // Black pieces
            board.fields[3, 4] = new Queen(ChessColor.Black); // D5
            board.fields[0, 7] = new Rook(ChessColor.Black); // A8
            board.fields[1, 7] = new Knight(ChessColor.Black); // B8
            board.fields[2, 7] = new Bishop(ChessColor.Black); // C8
            board.fields[4, 7] = new King(ChessColor.Black); // E8
            board.fields[6, 7] = new Knight(ChessColor.Black); // G8
            board.fields[7, 7] = new Rook(ChessColor.Black); // H8

            board.fields[0, 6] = new Pawn(ChessColor.Black); // A7
            board.fields[5, 6] = new Pawn(ChessColor.Black); // F7
            board.fields[6, 6] = new Pawn(ChessColor.Black); // G7
            board.fields[7, 6] = new Pawn(ChessColor.Black); // H7

            board.fields[2, 4] = new Bishop(ChessColor.Black); // C5
            
            // White pieces
            board.fields[2, 3] = new Bishop(ChessColor.White); // C4
            board.fields[6, 3] = new Queen(ChessColor.White); // G4
            board.fields[2, 2] = new Knight(ChessColor.White); // C3

            board.fields[0, 1] = new Pawn(ChessColor.White); // A2
            board.fields[1, 1] = new Pawn(ChessColor.White); // B2
            board.fields[6, 1] = new Pawn(ChessColor.White); // G2
            board.fields[7, 1] = new Pawn(ChessColor.White); // H2

            board.fields[0, 0] = new Rook(ChessColor.White); // A1
            board.fields[2, 0] = new Bishop(ChessColor.White); // C1
            board.fields[4, 0] = new King(ChessColor.White); // E1
            board.fields[6, 0] = new Knight(ChessColor.White); // G1
            board.fields[7, 0] = new Rook(ChessColor.White); // H1
            
            return board;
        }

        public XmlSchema? GetSchema() => null;


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("OnTurn", OnTurn.ToString());

            writer.WriteStartElement("allFields");

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = fields[x, y];
                    if (piece != null)
                    {
                        Coordinate c = new Coordinate(x, y);
                        writer.WriteStartElement(piece.GetType().Name); // e.g., "Pawn", "Knight"
                        writer.WriteAttributeString("color", piece.Color.ToString());
                        writer.WriteAttributeString("c", c.C.ToString());
                        writer.WriteAttributeString("z", c.Z.ToString());
                        writer.WriteEndElement();
                    }
                }
            }

            writer.WriteEndElement(); // </allFields>
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToAttribute("OnTurn");
            OnTurn = (ChessColor)Enum.Parse(typeof(ChessColor), reader.Value);
            reader.ReadStartElement("Board"); // enter Board

            reader.ReadStartElement("allFields"); // enter allFields
            while (reader.NodeType == XmlNodeType.Element)
            {
                string pieceType = reader.Name;
                ChessColor color = (ChessColor)Enum.Parse(typeof(ChessColor), reader.GetAttribute("color"));
                char c = char.Parse(reader.GetAttribute("c"));
                int z = int.Parse(reader.GetAttribute("z"));

                Coordinate coord = new Coordinate(0, 0) { C = c, Z = z };

                Piece piece = pieceType switch
                {
                    "Pawn" => new Pawn(color),
                    "Knight" => new Knight(color),
                    "Bishop" => new Bishop(color),
                    "Rook" => new Rook(color),
                    "Queen" => new Queen(color),
                    "King" => new King(color),
                    _ => null
                };

                if (piece != null)
                    fields[coord.X, coord.Y] = piece;

                reader.Read(); // next node
            }

            reader.ReadEndElement(); // </allFields>
            reader.ReadEndElement(); // </Board>
        }

        public static void SaveBoard(Board board, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Board));
            using FileStream fs = new FileStream(path, FileMode.Create);
            serializer.Serialize(fs, board);
        }

        public static Board LoadBoard(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Board));
            using FileStream fs = new FileStream(path, FileMode.Open);
            return (Board)serializer.Deserialize(fs);
        }

    }
}
