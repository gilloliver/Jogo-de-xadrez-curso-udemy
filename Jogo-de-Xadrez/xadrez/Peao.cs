﻿using System;
using tabuleiro;

namespace xadrez {
    class Peao : Peca {
        private PartidaDeXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base (cor, tab) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = tabuleiro.getPeca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos) {
            return tabuleiro.getPeca(pos) == null;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branco) {
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tabuleiro.posicaoNoTabuleiro(pos) && livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                Posicao p2 = new Posicao(posicao.Linha - 1, posicao.Coluna);
                if (tabuleiro.posicaoNoTabuleiro(p2) && livre(p2) && tabuleiro.posicaoNoTabuleiro(pos) && livre(pos) && qtdMovimento == 0) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                

                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoNoTabuleiro(pos) && existeInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoNoTabuleiro(pos) && existeInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogada especial en passant
                if (posicao.Linha == 3) {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tabuleiro.posicaoNoTabuleiro(esquerda) && existeInimigo(esquerda) && (tabuleiro.getPeca(esquerda) == partida.vuneravelEnPassant)) {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoNoTabuleiro(direita) && existeInimigo(direita) && (tabuleiro.getPeca(direita) == partida.vuneravelEnPassant)) {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            } else {
                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tabuleiro.posicaoNoTabuleiro(pos) && livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                if (tabuleiro.posicaoNoTabuleiro(pos) && livre(pos) && qtdMovimento == 0) {
                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoNoTabuleiro(pos) && existeInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoNoTabuleiro(pos) && existeInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogada especial en passant
                if (posicao.Linha == 4) {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tabuleiro.posicaoNoTabuleiro(esquerda) && existeInimigo(esquerda) && tabuleiro.getPeca(esquerda) == partida.vuneravelEnPassant) {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoNoTabuleiro(direita) && existeInimigo(direita) && tabuleiro.getPeca(direita) == partida.vuneravelEnPassant) {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
