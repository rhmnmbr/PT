import { Mahasiswa } from "./mahasiswa";
import { Dosen } from "./dosen";

export class Kelas {
    constructor(
        public KodeKelas: string,
        public Nama: string,
        public Dosen: Dosen[],
        public Mhss: Mahasiswa[],
        public NilaiMid: string,
        public NilaiSem: string,
        public NilaiMutu: string
    ) { }
}