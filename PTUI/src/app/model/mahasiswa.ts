import { Kelas } from '../model/kelas';

export class Mahasiswa {
    constructor(
        public Nim: number,
        public Nama: string,
        public TgLahir: string,
        public TpLahir: string,
        public JenisKelamin: string,
        public Klasses: Kelas[],
        public NilaiMid: string,
        public NilaiSem: string,
        public NilaiMutu: string
    ) { }
}