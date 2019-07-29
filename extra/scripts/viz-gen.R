library(ggbio)
library(GenomicRanges)
library(Gviz)
p.ideo <- Ideogram(genome = "hg19")
p.ideo + xlim(GRanges("chr2", IRanges(1e8, 1e8+10000000))) 

p <- p + circle(gr.crc1, geom = "link", linked.to = "to.gr", aes(color = rearrangements),
radius = 23)

p